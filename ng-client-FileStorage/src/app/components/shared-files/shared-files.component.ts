import { Component, OnInit } from '@angular/core';
import { FileService } from 'src/app/services/file.service';

@Component({
  selector: 'app-shared-files',
  templateUrl: './shared-files.component.html',
  styleUrls: ['./shared-files.component.scss'],
})
export class SharedFilesComponent implements OnInit {
  progress: number = 0;
  message: string = '';
  filesCurent: any[] = new Array();
  filesDefault: any[] = new Array();
  searchText = '';
  constructor(private fs: FileService) {}

  ngOnInit(): void {
    this.fs.GetSceletonOfPublicFiles().subscribe({
      next: (data) => {
        data.forEach((element: any) => {
          this.filesCurent.push(element);
          this.filesDefault.push(element);
        });
      },
    });
  }
  Search() {
    this.filesCurent = new Array();
    this.filesCurent = this.filesDefault;
    if (this.searchText != '')
      this.filesCurent = this.filesCurent.filter((res) => {
        return (
          res.fileName
            .toLocaleLowerCase()
            .match(this.searchText.toLocaleLowerCase()) ||
          res.username
            .toLocaleLowerCase()
            .match(this.searchText.toLocaleLowerCase()) ||
          res.createDate
            .toLocaleLowerCase()
            .match(this.searchText.toLocaleLowerCase())
        );
      });
  }
}
