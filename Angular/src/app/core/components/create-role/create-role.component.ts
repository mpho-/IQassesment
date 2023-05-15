import { Component } from '@angular/core';
import { User } from '../../models/User';
import { AuthService } from '../../services/AuthService';
import { UserService } from '../../services/UserService';
import { environment } from 'src/environments/environment';
import { RoleService } from '../../services/RoleService';
import { Role } from '../../models/Role';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-create-role',
  templateUrl: './create-role.component.html',
  styleUrls: ['./create-role.component.less']
})
export class CreateRoleComponent {
  roleCreate: Role = new Role();
 

  constructor(private authService: AuthService, private roleService: RoleService) {
  }


  onSubmit() {
    this.roleService.addRole(this.roleCreate.name).subscribe(() => {
      Swal.fire(
        'Role is created',
        '',
        'success'
      );
    });
  }
}