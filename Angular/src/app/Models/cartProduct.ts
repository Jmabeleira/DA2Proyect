import { Product } from "./product";

export interface CartProduct {
    id:string|null;
    product:Product;
    quantity:number;
}