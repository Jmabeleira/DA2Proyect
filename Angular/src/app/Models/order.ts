import { CartProduct } from "./cartProduct";
import { PaymentMethod } from "./paymentMethod";
import { Promotion } from "./promotion";
import { User } from "./user";

export interface Order{
    id: string|null;
    customer: User;
    date: string;
    products: CartProduct[];
    appliedPromotion: Promotion;
    paymentMethod: PaymentMethod;
    totalPrice: number;
}