import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { ACCESS_TOKEN_KEY } from 'src/app/services/auth.service';
import { FileService } from 'src/app/services/file.service';
@Component({
  selector: 'app-my-files',
  templateUrl: './my-files.component.html',
  styleUrls: ['./my-files.component.scss'],
})
export class MyFilesComponent implements OnInit {
  filesCurrent: any[] = new Array();
  filesDefault: any[] = new Array();
  searchText = '';
  constructor(
    private fs: FileService,
    private http: HttpClient,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.fs.GetSceletonOfAllFiles().subscribe({
      next: (data) => {
        data.forEach((element: any) => {
          this.filesCurrent.push(element);
          this.filesDefault.push(element);
        });
      },
    });
  }
  deleteItem(Id: number) {
    this.fs.deleteItem(Id).subscribe({
      next: () => {
        window.location.reload();
      },
      error: () =>{
        window.location.reload();
      }
    });
  }

  download(fileId: number) {
    this.fs.Download(fileId).subscribe({
      next: (data) => {
        this.downloadFile(data);
      }
    });
  }
  downloadFile(data: any) {
    let downloadLink = document.createElement('a');
    const byteCharacters = atob(data.fileStreamCol);

    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    downloadLink.href = window.URL.createObjectURL(
      new Blob([byteArray], { type: `application/${data.typeName}` })
    );
    if (data.fileName)
      downloadLink.setAttribute(
        'download',
        data.fileName + '.' + data.typeName
      );
    document.body.appendChild(downloadLink);
    downloadLink.click();
  }

  Search() {
    this.filesCurrent = new Array();
    this.filesCurrent = this.filesDefault;
    if (this.searchText != '')
      this.filesCurrent = this.filesCurrent.filter((res) => {
        return (
          res.fileName
            .toLocaleLowerCase()
            .match(this.searchText.toLocaleLowerCase()) ||
          res.username
            .toLocaleLowerCase()
            .match(this.searchText.toLocaleLowerCase()) ||
          res.createDate
            .toLocaleLowerCase()
            .match(this.searchText.toLocaleLowerCase()) ||
          res.fileSecureLevel
            .toLocaleLowerCase()
            .match(this.searchText.toLocaleLowerCase())
        );
      });
  }
}
