/**
* PurchaseOrderLineItem - container class for product PurchaseOrder line item
*/
export interface PurchaseOrderLineitem {
    id: number;
    purchaseOrderid: number;
    productid: string;
    qty: number;
    price:number;
   } 