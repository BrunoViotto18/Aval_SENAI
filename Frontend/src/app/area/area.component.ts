import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.css']
})
export class AreaComponent implements OnInit {

  veiculos: Array<number> = [1, 2, 3, 4, 5]

  constructor() { }

  ngOnInit(): void {
  }

}
