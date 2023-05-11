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

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AdministrationComponent,
    ProfileComponent,
    LoginComponent,
    CreateEditUserComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule
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
