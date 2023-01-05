import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { BookService, BookDto, bookTypeOptions, AuthorLookupDto } from '@proxy/books';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ProductService, ProductDto, productTypeOptions, CreateUpdateProductDto } from '@proxy/products';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})


export class ProductComponent implements OnInit {

  product = { items: [], totalCount: 0 } as PagedResultDto<ProductDto>;

  form: FormGroup;

  selectedProduct = {} as ProductDto;

  authors$: Observable<AuthorLookupDto[]>;

  type = productTypeOptions;

  isModalOpen = false;

  constructor(public readonly list: ListService,
    private productService: ProductService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService) { }

  ngOnInit(): void {
    const productStreamCreator = (query) => this.productService.getList(query);
    this.list.hookToQuery(productStreamCreator).subscribe((response) => {
      this.product = response;
    });
  }

  createProduct() {
    this.selectedProduct = {} as ProductDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editProduct(id: string) {
    this.productService.get(id).subscribe((product) => {
      this.selectedProduct = product;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  deleteProduct(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.productService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedProduct.name || null, Validators.required],
      type: [this.selectedProduct.type || null, Validators.required],

      price: [this.selectedProduct.price || null, Validators.required],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

  }



}
