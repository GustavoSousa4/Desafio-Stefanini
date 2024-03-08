import { ItensPedido } from "./itensPedido";

export interface Pedido {
    nomeCliente: string;
    emailCliente: string;
    pago: boolean;
    item: ItensPedido[];
}