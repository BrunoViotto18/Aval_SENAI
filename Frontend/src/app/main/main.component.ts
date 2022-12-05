import { Component, OnInit } from '@angular/core';
import { GlobalConstants } from "../../commom/global-constants"
import { Router } from '@angular/router';

import axios from 'axios';
import { Endpoints } from 'src/commom/endpoints';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit{

  ocupatedAreas: Array<number> = [];

  constructor(private router: Router) { }

  async ngOnInit(): Promise<void> {
    let self = this;

    await axios(Endpoints.getAllAllocatedAreasRequest)
    .then(function (response) {
      self.ocupatedAreas = response.data
    })
    .catch(function (error) {
      console.log(error);
      alert('Não foi possível se comunicar com o servidor.');
    });
    
    this.configAreas();
  }

  configAreas()
  {
    for (let area of this.ocupatedAreas){
      let div = document.body.querySelector(`[data_area='${area}']`) as HTMLDivElement;
      
      div.style.backgroundColor = GlobalConstants.css.color.azul;
      div.style.cursor = 'pointer'

      let self = this;
      div.onclick = function(){
        let number = document.body.querySelector(`.${div.classList[0]} > .number`) as HTMLDivElement
        self.router.navigate([`/area`, number.innerText])
      }
    }
  }
}
