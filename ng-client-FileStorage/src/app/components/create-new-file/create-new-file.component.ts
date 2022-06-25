import { Component, Output, OnInit, EventEmitter } from '@angular/core';
import { HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { FileService } from 'src/app/services/file.service';
@Component({
  selector: 'app-create-new-file',
  templateUrl: './create-new-file.component.html',
  styleUrls: ['./create-new-file.component.scss'],
})
export class CreateNewFileComponent implements OnInit {
  progress: number = 0;
  message: string = '';
  typeOfFile: string = 'public';
  @Output() public onUploadFinished = new EventEmitter();
  constructor(private router: Router, private fs: FileService) {}
  ngOnInit(): void {}
  FileType(type: string) {
    this.typeOfFile = type;
  }
 
  back() {
    this.router.navigate(['/my-files']);
  }
  uploadFile = (files: any) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File[]>files;
    const formData = new FormData();
    for (let i = 0; i < fileToUpload.length; i++) {
      formData.append('file', fileToUpload[i], fileToUpload[i].name);
    }
    this.fs.CreateFile(this.typeOfFile, formData).subscribe({
      next: (event) => {
        if (
          event.type === HttpEventType.UploadProgress &&
          event.total != null
        ) {
          this.progress = Math.round((100 * event.loaded) / event.total);
          if (this.progress == 100) {
            this.message = 'Upload success';
            this.progress = 0;
          }
        } else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success';
          this.onUploadFinished.emit(event.body);
        }
      },
      error: (err: HttpErrorResponse) => console.log(err),
    });
  };
}
