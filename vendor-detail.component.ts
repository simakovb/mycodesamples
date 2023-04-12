import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import {
 FormControl,
 FormGroup,
 FormBuilder,
 Validators
} from '@angular/forms';
import { Vendor } from '../vendor';
import { ValidatePostalcode } from '../../validators/postalcode.validator';
import { ValidatePhone } from '@app/validators/phoneno.validator';
import { DeleteDialogComponent } from '@app/delete-dialog/delete-dialog.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';

@Component({
 selector: 'app-vendor-detail',
 templateUrl: './vendor-detail.component.html',
})
export class VendorDetailComponent implements OnInit {
 @Input() selectedVendor: Vendor = {
 id: 0,
 address1: '',
 city: '',
 email: '',
 name: '',
 phone: '',
 postalcode: '',
 province: '',
 type: ''
 };
 @Input() vendors: Vendor[] | null = null;
 @Output() cancelled = new EventEmitter();
 @Output() deleted = new EventEmitter();
 @Output() saved = new EventEmitter();
 vendorForm: FormGroup;
 address1: FormControl;
 city: FormControl;
 email: FormControl;
 name: FormControl;
 phone: FormControl;
 postalcode: FormControl;
 province: FormControl;
 type: FormControl;

 constructor(private builder: FormBuilder, private dialog: MatDialog) {
 this.address1 = new FormControl('', Validators.compose([Validators.required]));
 this.city = new FormControl('', Validators.compose([Validators.required]));
 this.email = new FormControl('', Validators.compose([Validators.required, Validators.email]));
 this.name = new FormControl('', Validators.compose([Validators.required]));
 this.phone = new FormControl('', Validators.compose([Validators.required, ValidatePhone]));
 this.postalcode = new FormControl('', Validators.compose([Validators.required, ValidatePostalcode]));
 this.province = new FormControl('', Validators.compose([Validators.required]));
 this.type = new FormControl('', Validators.compose([Validators.required]));
 this.vendorForm = new FormGroup({
  address1: this.address1,
  city: this.city,
  email: this.email,
  name: this.name,
  phone: this.phone,
  postalcode: this.postalcode,
  province: this.province,
  type: this.type,
 });
 } // constructor

 ngOnInit(): void {
 // patchValue doesn’t care if all values present
 this.vendorForm.patchValue({
 address1: this.selectedVendor.address1,
 city: this.selectedVendor.city,
 email: this.selectedVendor.email,
 name: this.selectedVendor.name,
 phone: this.selectedVendor.phone,
 postalcode: this.selectedVendor.postalcode,
 province: this.selectedVendor.province,
 type: this.selectedVendor.type,
 });
 } // ngOnInit
 updateSelectedVendor(): void {
 this.selectedVendor.address1 = this.vendorForm.value.address1;
 this.selectedVendor.city = this.vendorForm.value.city;
 this.selectedVendor.email = this.vendorForm.value.email;
 this.selectedVendor.name = this.vendorForm.value.name;
 this.selectedVendor.phone = this.vendorForm.value.phone;
 this.selectedVendor.postalcode = this.vendorForm.value.postalcode;
 this.selectedVendor.province = this.vendorForm.value.province;
 this.selectedVendor.type = this.vendorForm.value.type;
 this.saved.emit(this.selectedVendor);
 }
 
openDeleteDialog(selectedVendor: Vendor): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    dialogConfig.data = {
    title: `Delete Vendor ${this.selectedVendor.id}`,
    entityname: 'expense'
    };
    dialogConfig.panelClass = 'customdialog';
    const dialogRef = this.dialog.open(DeleteDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
    if (result) {
    this.deleted.emit(this.selectedVendor);
    }
    });
    } // openDeleteDialog
} // VendorDetailComponent
