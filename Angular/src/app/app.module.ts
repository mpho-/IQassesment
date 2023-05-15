import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { APIInterceptor } from './APIInterceptor';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdministrationComponent } from './core/components/administration/administration.component';
import { CreateEditUserComponent } from './core/components/create-edit-user/create-edit-user.component';
import { LoginComponent } from './core/components/login/login.component';
import { ProfileComponent } from './core/components/profile/profile.component';
import { AuthGuard } from './core/guards/AuthGuard';
import { RoleComponent } from './core/components/roles/roles.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { CreateRoleComponent } from './core/components/create-role/create-role.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AdministrationComponent,
    ProfileComponent,
    LoginComponent,
    CreateEditUserComponent,
    RoleComponent,
    CreateRoleComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    NgSelectModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: APIInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
