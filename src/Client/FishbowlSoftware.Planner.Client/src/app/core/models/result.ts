/**
 * API result model
 */
export interface Result<T = unknown> {
  isSuccess: boolean;
  data?: T;
  error?: string;
}
