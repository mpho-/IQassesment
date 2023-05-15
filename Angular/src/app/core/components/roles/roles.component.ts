import { Component } from '@angular/core';
import { User } from '../../models/User';
import { AuthService } from '../../services/AuthService';
import { UserService } from '../../services/UserService';
import { RoleService } from '../../services/RoleService';
import { Role } from '../../models/Role';
@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.less']
})
export class RoleComponent {
  roles!: Role[];
  user!: User;
  profilePicture!: File;

  constructor(private authService: AuthService, private userService: UserService, private roleService: RoleService) {
    this.userService.getCurrentUser().subscribe(user => {
      this.user = user;
    });
    this.getRoles();
  }

  getRoles() {
    this.roleService.getAllRoles().subscribe(res => {
     this.roles = res;
    });
  }

  addRole(name :string) {
    this.roleService.addRole(name).subscribe(res => {});
  }
}