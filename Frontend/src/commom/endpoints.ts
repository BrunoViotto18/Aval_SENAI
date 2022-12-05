import { GlobalConstants } from "./global-constants";


export class Endpoints{
    /* Alocação */

    public static getAllAllocatedAreasRequest = {
        method: 'get',
        url: `${GlobalConstants.apiControllers.alocacao}/GetAllAllocatedAreas`,
        headers: {
            'Content-Type': 'application/json'
        }
    };

    public static GetAllAutomobilesInArea(area: number){
        return {
            method: 'get',
            url: `${GlobalConstants.apiControllers.alocacao}/GetAllAutomobilesAllocatedInArea/${area}`,
            headers: {
                'Content-Type': 'application/json'
            }
        }
    }

    public static GetAlocacaoIdFromForeignKeys(data: any) {
        return {
            method: 'post',
            url: `${GlobalConstants.apiControllers.alocacao}/GetAlocacaoIdFromForeignKeys`,
            headers: { 
              'Content-Type': 'application/json'
            },
            data : data
        }
    }

    
    /* Automovel */

    public static GetAutomobileModelFromId(id: number){
        return {
            method: 'get',
            url: `${GlobalConstants.apiControllers.automovel}/GetAutomobileModelFromId/${id}`,
            headers: {
                'Content-Type': 'application/json'
            }
        }
    }


    /* Cliente */

    public static GetAllClientes = {
        method: 'get',
        url: `${GlobalConstants.apiControllers.cliente}/GetAllClients`,
        headers: {
            'Content-Type': 'application/json'
        }
    }


    /* Concessionaria */

    public static GetConcessionariasWithModelAllocatedInArea(data: any) {
        return {
            method: 'post',
            url: `${GlobalConstants.apiControllers.concessionaria}/GetConcessionariasWithModelAllocatedInArea`,
            headers: {
                'Content-Type': 'application/json'
            },
            data: data,
        }
    }


    /* Venda */

    public static RegisterVenda(data: any){
        return {
            method: 'post',
            url: 'http://localhost:5001/Venda/RegisterVenda',
            headers: { 
                'Content-Type': 'application/json'
            },
            data : data
        };
    }
}
