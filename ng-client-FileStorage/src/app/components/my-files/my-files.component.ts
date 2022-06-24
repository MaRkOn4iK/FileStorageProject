import { Component, OnInit } from '@angular/core';
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
  constructor(private fs: FileService) {}
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
    this.fs.deleteItem(Id).subscribe();
    window.location.reload();
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
