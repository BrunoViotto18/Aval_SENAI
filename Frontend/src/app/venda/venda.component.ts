import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Endpoints } from 'src/commom/endpoints';
import { Cliente, Concessionaria } from 'src/model';

import axios from 'axios';


@Component({
  selector: 'app-venda',
  templateUrl: './venda.component.html',
  styleUrls: ['./venda.component.css']
})
export class VendaComponent implements OnInit {

  id: number = 0
  area: number = 0
  modelo: string = ''
  alocacaoId: number = 0
  clientes: Array<Cliente> = []
  concessionarias: Array<Concessionaria> = []

  constructor(private route: ActivatedRoute, private router: Router) { }

  async ngOnInit(): Promise<void> {
    const routeParams = this.route.snapshot.paramMap
    this.id = Number(routeParams.get('automovelId'))

    this.area = Number(window.history.state['area'])
    if (this.area == null)
      this.router.navigate([''])
    
    let self = this

    await axios(Endpoints.GetAutomobileModelFromId(this.id))
    .then(function (response) {
      self.modelo = response.data
    })
    .catch(function (error) {
      alert('Não foi possível se comunicar com o servidor.')
      return
    });

    axios(Endpoints.GetAllClientes)
    .then(function (response) {
      self.clientes = response.data
    })
    .catch(function (error) {
      alert('Não foi possível se comunicar com o servidor.')
      return
    });

    let data = JSON.stringify({
      "modelo": this.modelo,
      "area": this.area
    })
    axios(Endpoints.GetConcessionariasWithModelAllocatedInArea(data))
    .then(function (response) {
      self.concessionarias = response.data
    })
    .catch(function (error) {
      alert('Não foi possível se comunicar com o servidor.')
      return
    });

  }

  async sell(){
    let clienteSelect = document.body.querySelector('#cliente') as HTMLSelectElement
    let concessionariaSelect = document.body.querySelector('#concessionaria') as HTMLSelectElement

    let self = this

    let data = JSON.stringify({
      "clientId": clienteSelect.value,
      "automovelId": this.id,
      "concessionariaId": concessionariaSelect.value 
    })
    axios(Endpoints.RegisterVenda(data))
    .then(function (response) {
      self.router.navigate(['/area', self.area])
    })
    .catch(function (error) {
      console.log(error);
    });
  }

}
