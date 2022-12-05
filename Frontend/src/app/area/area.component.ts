import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationExtras, Route, Router } from '@angular/router';
import { Automovel } from 'src/model';

import axios from 'axios';
import { Endpoints } from 'src/commom/endpoints';
import { Target } from '@angular/compiler';


@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.css']
})
export class AreaComponent implements OnInit {

  area: number = 0
  veiculos: Array<Automovel> = []

  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    this.area = Number(routeParams.get('area'));

    let self = this
    axios(Endpoints.GetAllAutomobilesInArea(this.area))
    .then(function (response) {
      self.veiculos = response.data

      if (self.veiculos.length == 0)
        self.router.navigate(['/'])
    })
    .catch(function (error) {
      console.log(error);
      alert('Não foi possível se comunicar com o servidor.');
    });

  }

  redirectToVenda(veiculoId: number){
    let navigationExtras: NavigationExtras = {
      state: {
        area: this.area
      }
    };

    this.router.navigate(['/venda', veiculoId], navigationExtras);
  }

}
