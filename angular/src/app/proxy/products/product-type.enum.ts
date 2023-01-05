import { mapEnumToOptions } from '@abp/ng.core';

export enum ProductType {
  Undefined = 0,
  Shirt = 1,
  Trousers = 2,
}

export const productTypeOptions = mapEnumToOptions(ProductType);
