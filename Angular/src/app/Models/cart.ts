import { CartProduct } from "./cartProduct";
import { Promotion } from "./promotion";
import { User } from "./user";

export interface Cart{
    id:string|null;
    user:User;
    products:CartProduct[];
    promotion:Promotion|null;
    totalPrice:number;
}