import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Vendor } from '@app/vendor/vendor';
import { Product } from '@app/product/product';
import { PurchaseOrderLineitem } from '@app/purchase-order/purchase-order-lineitem';
import { PurchaseOrder } from '@app/purchase-order/purchase-order';
import { VendorService } from '@app/vendor/vendor.service';
import { ProductService } from '@app/product/product.service';
import { PurchaseOrderService } from '@app/purchase-order/purchase-order.service';
import { PDFURL } from '@app/constants';
@Component({
 templateUrl: './generator.component.html',
})
export class GeneratorComponent implements OnInit, OnDestroy {
 // form
 generatorForm: FormGroup;
 vendorid: FormControl;
 productid: FormControl;
 quantityid: FormControl;
 // data
 formSubscription?: Subscription;
 products$?: Observable<Product[]>; // everybody's products
 quantitys: Object[] =["eoq",0,1,2,3,4,5,6,7,8,9,10]; // everybody's products
 vendors$?: Observable<Vendor[]>; // all vendors
 vendorproducts$?: Observable<Product[]>; // all products for a particular vendor
 items: Array<PurchaseOrderLineitem>; // product items that will be in purchaseOrder
 selectedproducts: Product[]; // products that being displayed currently in app
 selectedProduct: Product; // the current selected product
 selectedVendor: Vendor; // the current selected vendor
 // misc
 pickedProduct: boolean;
 pickedVendor: boolean;
 generated: boolean;
 hasProducts: boolean;
 hasQuantity:boolean;
 msg: string;
 total: number;
 tax: number;
 subtotal: number;
 purchaseOrderno: number = 0;
 selectedQty: number[];
 qtyCount:number;
 constructor(
 private builder: FormBuilder,
 private vendorService: VendorService,
 private productService: ProductService,
 private purchaseOrderService: PurchaseOrderService
 ) {
 this.pickedVendor = false;
 this.pickedProduct = false;
 this.generated = false;
 this.msg = '';
 this.vendorid = new FormControl('');
 this.productid = new FormControl('');
 this.quantityid = new FormControl('');
 this.generatorForm = this.builder.group({
 productid: this.productid,
 vendorid: this.vendorid,
 quantityid: this.quantityid,
 });
 this.selectedProduct = {
  id: '0',
  vendorid: 0,
  name: '',
  costprice: 0.0,
  msrp: 0.0,
  rop: 0,
  eoq: 0,
  qoh: 0,
  qoo:0,
  qrcode: '',
  qrcodetxt: '',
 };
 this.selectedVendor = {
  id: 0,
  address1: '',
  city: '',
  province: '',
  postalcode: '',
  phone: '',
  type: '',
  name: '',
  email: '',
 };
 this.selectedQty = [];
 this.qtyCount=0;
 this.items = new Array<PurchaseOrderLineitem>();
 this.selectedproducts = new Array<Product>();
 this.hasProducts = false;
 this.hasQuantity= false;
 this.total = 0.0;
 this.tax = 0.0;
 this.subtotal = 0.0;
 } // constructor
 ngOnInit(): void {
 this.onPickVendor();
 this.onPickProduct();
 this.onPickQuantity();
 this.msg = 'loading vendors and products from server...';
 (this.vendors$ = this.vendorService.get()),
 catchError((err) => (this.msg = err.message));
 (this.products$ = this.productService.get()),
 catchError((err) => (this.msg = err.message));
 this.msg = 'server data loaded';
 } // ngOnInit
 ngOnDestroy(): void {
 if (this.formSubscription !== undefined) {
 this.formSubscription.unsubscribe();
 }
 } // ngOnDestroy
 /**
 * onPickVendor - Another way to use Observables, subscribe to the select change event
 * then load specific vendor products for subsequent selection
 */
 onPickVendor(): void {
 this.formSubscription = this.generatorForm
 .get('vendorid')
 ?.valueChanges.subscribe((val) => {
 this.selectedProduct = {
  id: '0',
  vendorid: 0,
  name: '',
  costprice: 0.0,
  msrp: 0.0,
  rop: 0,
  eoq: 0,
  qoh: 0,
  qoo: 0,
  qrcode: '',
  qrcodetxt: '',
 };
 this.selectedVendor = val;
 this.loadVendorProducts();
 this.pickedProduct = false;
 this.hasProducts = false;
 this.hasQuantity = false;
 this.msg = 'choose product for vendor';
 this.pickedVendor = true;
 this.generated = false;
 this.items = []; // array for the purchaseOrder
 this.selectedproducts = []; // array for the details in app html
 });
 } // onPickVendor
 /**
 * onPickProduct - subscribe to the select change event then
 * update array containing items.
 */
 onPickProduct(): void {
 const productSubscription = this.generatorForm
 .get('productid')
 ?.valueChanges.subscribe((val) => {
 this.selectedProduct = val;
 const item: PurchaseOrderLineitem = {
 id: 0,
 purchaseOrderid: 0,
 productid: this.selectedProduct?.id,
 qty:0,
 price:0
 };
 if (
 this.items.find((item) => item.productid === this.selectedProduct?.id)
 ) {
 // ignore entry
 } else {
 // add entry
 this.items.push(item);
 this.selectedproducts.push(this.selectedProduct);
 }
 if (this.items.length > 0) {
 this.hasProducts = true;
 this.hasQuantity=false;
 }
 this.total = 0.0;
 
 });
 this.formSubscription?.add(productSubscription); // add it as a child, so all can be destroyed together
 } // onPickProduct
 onPickQuantity(): void {
    const productSubscription = this.generatorForm
   .get('quantityid')
   ?.valueChanges.subscribe((val) => {
    if(val === 'eoq'){
        val=this.selectedProduct.eoq;
    }
    this.selectedQty[this.qtyCount] = val;
    const item: PurchaseOrderLineitem = {
      id: 0,
      purchaseOrderid: 0,
      qty: this.selectedQty[this.qtyCount],
      price: this.selectedProduct.costprice,
      productid: this.selectedProduct?.id,
    };
    const i = this.items.findIndex((item) => item.productid === this.selectedProduct?.id);
    if (
          this.items.find((item) => item.productid === this.selectedProduct?.id)
        ) {
            item.price += item.price * item.qty;
            this.items[i] = item;
        } else {
          // add entry
          this.items.push(item);
          this.selectedproducts.push(this.selectedProduct);
        }
        if (this.items.length > 0) {
          this.hasProducts = true;
        }
        if (item.qty !=0 && item.qty !=11){
          this.hasQuantity=true;
        }
        this.total = 0.0;
        this.subtotal = 0.0;
        this.tax = 0.0;
        this.selectedproducts.forEach((exp, i) => (this.subtotal += exp.costprice*this.selectedQty[i]));
        this.tax= 0.13*this.subtotal; 
        this.total=this.subtotal+this.tax;
        this.qtyCount++;
      });
      this.formSubscription?.add(productSubscription);
} // onPickProduct
 /**
 * loadVendorProducts - filter for a particular vendor's products
 */
 loadVendorProducts(): void {
 this.vendorproducts$ = this.products$?.pipe(
 map((products) =>
 // map each product in the array and check whether or not it belongs to selected vendor
 products.filter(
 (product) => product.vendorid === this.selectedVendor?.id
 )
 )
 );
 } // loadVendorProducts
 /**
 * createPurchaseOrder - create the client side purchaseOrder
 */
 createPurchaseOrder(): void {
 this.generated = false;
 const purchaseOrder: PurchaseOrder = {
 id: 0,
 items: this.items,
 vendorid: this.selectedProduct.vendorid,
 };
 this.purchaseOrderService.add(purchaseOrder).subscribe({
 // observer object
 next: (purchaseOrder: PurchaseOrder) => {
 // server should be returning purchaseOrder with new id
 purchaseOrder.id > 0
 ? (this.msg = `PurchaseOrder ${purchaseOrder.id} added!`)
 : (this.msg = 'PurchaseOrder not added! - server error');
 this.purchaseOrderno = purchaseOrder.id;
 },
 error: (err: Error) => (this.msg = `PurchaseOrder not added! - ${err.message}`),
 complete: () => {
 this.hasProducts = false;
 this.hasQuantity= false;
 this.pickedVendor = false;
 this.pickedProduct = false;
 this.generated = true;
 },
 });
 } // createPurchaseOrder
 viewPdf(): void {
    window.open(`${PDFURL}${this.purchaseOrderno}`, '');
    } // viewPdf
   
} // GeneratorComponent