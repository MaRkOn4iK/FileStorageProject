import { Component } from '@angular/core';
import { FileService } from 'src/app/services/file.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  constructor(private fs: FileService) {
   this.fs.GetSceletonOfPublicFiles().subscribe();}
}
