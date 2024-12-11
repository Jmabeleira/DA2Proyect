import { CartProduct } from "../cartProduct";
import { User } from "../user";


export interface CartIn{
    user:User;
    cartProducts:CartProduct[];
}