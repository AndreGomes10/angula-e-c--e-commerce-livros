import { Component, OnInit } from '@angular/core';
import { BooksService } from './product-list.component.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  livros: any;
  booksService: BooksService;
  constructor(booksService: BooksService) {
    this.booksService = booksService;

    // this.booksService = new BooksService();
  }

  ngOnInit(): void {
    // ir na API e salvar o retorno dela em alguma variavel
    this.livros = this.booksService.getBooks().subscribe((data => {
      this.livros = data;
      console.log(this.livros);
    }))
  }

}
