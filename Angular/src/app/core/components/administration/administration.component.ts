import { Component, OnInit } from '@angular/core';
import { User } from '../../models/User';
import { UserService } from '../../services/UserService';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-administration',
  templateUrl: './administration.component.html',
  styleUrls: ['./administration.component.less']
})
export class AdministrationComponent implements OnInit {
  users: User[] = [];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.getAllUsers().subscribe(users => this.users = users);
  }


  deleteUser(user: User) {
    Swal.fire({
      title: 'Are you sure?',
      icon: 'info',
      confirmButtonText: 'Delete'
    }).then((result) => {
      if (result['isConfirmed']){
        this.userService.deleteUser(user).subscribe(() => {
         
          const index = this.users.findIndex(u => u.id === user.id);
          this.users.splice(index, 1);
        });
      }
    })
 
  }
}
