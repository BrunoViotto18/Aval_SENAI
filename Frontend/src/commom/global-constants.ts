export class GlobalConstants{
    public static apiURL: string = 'http://localhost:5001'

    public static apiControllers = {
        alocacao: `${this.apiURL}/Alocacao`,
        automovel: `${this.apiURL}/Automovel`,
        cliente: `${this.apiURL}/Cliente`,
        concessionaria: `${this.apiURL}/Concessionaria`,
        venda: `${this.apiURL}/Venda`
    };

    public static header = {
        contentType: 'application/json'
    };

    
    public static style = window.getComputedStyle(document.documentElement)

    public static css = {
        color: {
            branco: this.style.getPropertyValue('--Branco'),
            roxo: this.style.getPropertyValue('--Roxo'),
            azul: this.style.getPropertyValue('--Azul'),
            cinza: this.style.getPropertyValue('--Cinza'),
            preto: this.style.getPropertyValue('--Preto')
        }
    }
}
