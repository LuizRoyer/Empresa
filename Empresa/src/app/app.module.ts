import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { EnderecoListComponent } from './Endereco/endereco-list.component';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from './NavBar/nav-bar.component';
import { Error404Component } from './404/error404.component';
import { HttpClientModule } from '@angular/common/http';
import { EnderecoInfoComponent } from './Endereco/endereco.info.component';
import { EnderecoCreateComponent } from './Endereco/endereco.create.component';
import { FornecedorListComponent } from './Fornecedor/fornecedor-list.component';
import { EmpresaListComponent } from './Empresa/empresa-list.component';
import { EmpresaCreateComponent } from './Empresa/empresa-create.component';
import { PesFisicaListComponent } from './Representante/PessoaFisica/pesFisico-list.component';
import { PesJuridicoListComponent } from './Representante/PessoaJuridica/pesJuridico-list.component';
import { ContatoInfoComponent } from './Contato/contato.info.component';
import { PesFisicaCreateComponent } from './Representante/PessoaFisica/pesFisica.create.component';
import { ContatoListComponent } from './Contato/contato-list.component';
import { PesJuridicoCreateComponent } from './Representante/PessoaJuridica/pesJuridico.create.component';
import { FornecedorCreateComponent } from './Fornecedor/fornecedor.create.component';

@NgModule({
  declarations: [
    AppComponent,
    EnderecoListComponent,
    EnderecoCreateComponent,
    EnderecoInfoComponent,
    NavbarComponent,
    Error404Component,
    EmpresaListComponent,
    EmpresaCreateComponent,
    FornecedorListComponent,
    PesJuridicoListComponent,
    PesJuridicoCreateComponent,
    PesFisicaListComponent,
    PesFisicaCreateComponent,
    ContatoInfoComponent,
    ContatoListComponent,
    FornecedorCreateComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'fornecedor', pathMatch: 'full' },
      { path: 'endereco', component: EnderecoListComponent },
      { path: 'endereco/create', component: EnderecoCreateComponent },
      { path: 'endereco/info/:id', component: EnderecoInfoComponent },
      { path: 'fornecedor', component: FornecedorListComponent },
      { path: 'fornecedor/create', component: FornecedorCreateComponent },
      { path: 'empresa', component: EmpresaListComponent },
      { path: 'empresa/create', component: EmpresaCreateComponent },
      { path: 'fisica', component: PesFisicaListComponent },
      { path: 'fisica/create', component: PesFisicaCreateComponent },
      { path: 'juridica', component: PesJuridicoListComponent },
      { path: 'juridica/create', component: PesJuridicoCreateComponent },
      { path: 'contato/info/:id', component: ContatoInfoComponent },
      { path: 'contato/:id', component: ContatoListComponent },
      { path: '**', component: Error404Component }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
