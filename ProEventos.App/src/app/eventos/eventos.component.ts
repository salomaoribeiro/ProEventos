import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  private _filtroLista: string = '';
  public _todosEventos: any = [];
  public eventos: any = [];
  imageWidth: number = 150;
  imageMargin: number = 2;
  mostrarImagens: boolean = false;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  public get filtroLista()
  {
    return this._filtroLista;
  }

  public set filtroLista(value: string)
  {
    this._filtroLista = value;
    this.eventos = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this._todosEventos;
  }

  private filtrarEventos(filtro: string) : any
  {
    filtro = filtro.toLocaleLowerCase();
    return this._todosEventos.filter(
      (evento: {local: string ; tema: string; }) => evento.tema.toLocaleLowerCase().indexOf(filtro) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtro) !== -1
    )
  }

  public getEventos(){
    this.http.get("http://localhost:5268/api/eventos").subscribe(
      response => {
        this._todosEventos = response;
        this.eventos = response;
      },
      error => console.log(error)
    );
  }

  public mostrarImagem()
  {
    this.mostrarImagens = !this.mostrarImagens;
  }

}
