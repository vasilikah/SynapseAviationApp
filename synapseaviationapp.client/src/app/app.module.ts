import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { SearchFilterComponent } from './components/search-filter/search-filter.component';
import { UserDialogComponent } from './components/user-dialog/user-dialog.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { ToastrModule } from 'ngx-toastr';


@NgModule({
  declarations: [
    AppComponent,
    UserListComponent,
    SearchFilterComponent,
    UserDialogComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, FormsModule, NoopAnimationsModule, MatDialogModule, ToastrModule.forRoot(), ReactiveFormsModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
