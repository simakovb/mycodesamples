import { PurchaseOrderLineitem } from './purchase-order-lineitem';
/**
* PurchaseOrder - interface for expense report
*/
export interface PurchaseOrder {
 id: number;
 vendorid: number;
 items: PurchaseOrderLineitem[];
 podate?: string;
}