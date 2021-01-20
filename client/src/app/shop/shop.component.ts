import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IAuthor } from '../shared/models/author';
import { ICategory } from '../shared/models/category';
import { IProduct } from '../shared/models/product';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', {static: true}) searchTerm: ElementRef;

  products: IProduct[];
  category: ICategory[];
  author: IAuthor[];
  shopParams = new ShopParams();
  totalCount: number;
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low -> High', value: 'priceAsc'},
    {name: 'Price: High -> Low', value: 'priceDesc'}
  ];


  constructor(private shopService: ShopService) { }

  ngOnInit()  {
    this.getProducts();
    this.getCategory();
    this.getAuthor();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(response => {
    this.products = response.data;
    this.shopParams.pageNumber = response.pageIndex;
    this.shopParams.pageSize = response.pageSize;
    this.totalCount = response.count;
    }, error => {
      console.log(error);
    });

  }

  getCategory() {
    this.shopService.getCategory().subscribe(response => {
      this.category = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  getAuthor() {
    this.shopService.getAuthor().subscribe(response => {
      this.author = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  onCategorySelected(categoryId: number) {
    this.shopParams.categoryId = categoryId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onAuthorSelected(authorId: number) {
    this.shopParams.authorId = authorId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: any) {
    if (this.shopParams.pageNumber !== event)
    {
      this.shopParams.pageNumber = event;
      this.getProducts(); }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }


}
