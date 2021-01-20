import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAuthor } from '../shared/models/author';
import { ICategory } from '../shared/models/category';
import { IPagination } from '../shared/models/pagination';
import {map} from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {}

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.categoryId !== 0) {
      params = params.append('categoryId', shopParams.categoryId.toString());
    }

    if (shopParams.authorId !== 0) {
      params = params.append('authorId', shopParams.authorId.toString());
    }

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageIndex', shopParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'products', {observe: 'response', params})
    .pipe(
      map(response => {
        return response.body;
      })
    );
  }

  getCategory() {
     return this.http.get<ICategory[]>(this.baseUrl + 'products/category');
  }

  getAuthor() {
    return this.http.get<IAuthor[]>(this.baseUrl + 'products/author');
 }
}
