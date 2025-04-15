import { Component, OnInit, TemplateRef } from '@angular/core';

import { EventoService } from '../../services/Evento.service';
import { Evento } from '../../Models/Evento';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})

export class EventosComponent implements OnInit {
 
  constructor() { }

  ngOnInit(): void {}
}
