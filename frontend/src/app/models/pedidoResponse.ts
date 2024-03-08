import { ItensPedidoResponse } from "./itensPedidoResponse";

export interface PedidoResponse {
    id: number;
    nomeCliente: string;
    emailCliente: string;
    pago: boolean;
    valorTotal: number;
    itensPedidos: ItensPedidoResponse[];
}