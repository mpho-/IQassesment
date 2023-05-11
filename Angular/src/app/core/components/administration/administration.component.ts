import { Component, OnInit } from '@angular/core';
import { User } from '../../models/User';
import { UserService } from '../../services/UserService';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.less']
})
export class AdministrationComponent implements OnInit {
  users: User[] = [];
  roles: string[] = ['Manager', 'Employee'];
  projects: string[] = ['Project A', 'Project B', 'Project C'];
  selectedUser: User = new User();
  selectedProject: number = 0;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.getAllUsers().subscribe(users => this.users = users);
  }


  deleteUser(user: User) {
    this.userService.deleteUser(user).subscribe(() => {
      const index = this.users.findIndex(u => u.id === user.id);
      this.users.splice(index, 1);
    });
  }
}
