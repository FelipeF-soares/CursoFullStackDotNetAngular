import { Component, OnInit, TemplateRef } from '@angular/core';

import { EventoService } from '../../../services/Evento.service';
import { Evento } from '../../../Models/Evento';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';



@Component({
  selector: 'app-eventos-lista',
  templateUrl: './eventos-lista.component.html',
  styleUrls: ['./eventos-lista.component.scss']
})
export class EventosListaComponent implements OnInit {
  modalRef ={} as BsModalRef;
  public eventos: Evento[] =[];
  public eventosFiltrados :any;
  public isCollapsed:boolean = false;
  private _filtroLista:string = "";
 
  
  constructor(
    private eventoService:EventoService, 
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }
  
  public ngOnInit(): void {
    this.spinner.show();
    this.getEventos();
    
  }
  
  public get filtroLista() : string{
    return this._filtroLista;
  }
  
  public set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos
  }
  public filtrarEvento(filtrarPor: string):Evento[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento : any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 || 
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }
  
  
  public getEventos(): void {
    this.eventoService.getEventos().subscribe(
      {
        next: (eventos: Evento[]) =>
          {
          this.eventos = eventos;
          this.eventosFiltrados = this.eventos;
        },
        error: (error: any) => {
          this.spinner.hide(),
          this.toastr.error('Erro ao carregar os eventos','ERRO!')
        },
        complete:() => this.spinner.hide()
      }
    );
  }
  openModal(template: TemplateRef<any>):void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }
  
  confirm(): void {
    this.toastr.success('O Evento foi excluído com sucesso.', 'Excluído')
    this.modalRef.hide();
  }
  
  decline(): void {
    
    this.modalRef.hide();
  }
  detalheEvento(id:number): void{
    this.router.navigate([`eventos/detalhe/${id}`]);
  }
  
}

