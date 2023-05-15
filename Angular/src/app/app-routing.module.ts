import { createComponent, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministrationComponent } from './core/components/administration/administration.component';
import { CreateEditUserComponent } from './core/components/create-edit-user/create-edit-user.component';
import { LoginComponent } from './core/components/login/login.component';
import { ProfileComponent } from './core/components/profile/profile.component';
import { AuthGuard } from './core/guards/AuthGuard';
import { RoleGuard } from './core/guards/RoleGuard';
import { RoleComponent } from './core/components/roles/roles.component';
import { CreateRoleComponent } from './core/components/create-role/create-role.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'roles', component: RoleComponent, canActivate: [AuthGuard, RoleGuard], data: { roles: ['Manager'] } },
  { path: 'create-role', component: CreateRoleComponent, canActivate: [AuthGuard, RoleGuard], data: { roles: ['Manager'] } },
  { path: 'users', component: AdministrationComponent, canActivate: [AuthGuard, RoleGuard], data: { roles: ['Manager'] } },
  { path: 'create-edit', component: CreateEditUserComponent, canActivate: [AuthGuard, RoleGuard], data: { roles: ['Manager'] } },
  { path: 'create-edit/:id', component: CreateEditUserComponent, canActivate: [AuthGuard, RoleGuard], data: { roles: ['Manager'] } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
