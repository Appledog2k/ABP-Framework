import type { ProductType } from './product-type.enum';
import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateProductDto {
  name?: string;
  type: ProductType;
  createdDate: string;
  price: number;
}

export interface ProductDto extends AuditedEntityDto<string> {
  name?: string;
  type: ProductType;
  createdDate?: string;
  price: number;
}
