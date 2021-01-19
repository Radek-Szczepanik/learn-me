export interface Pagination {
    curentPage: number;
    itemsPerPage: number;
    totalItems: number;
    totalPages: number;
}

export class PaginationResult<T> {
    result: T;
    pagination: Pagination;
}