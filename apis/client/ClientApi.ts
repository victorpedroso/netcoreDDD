/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface AuthModel {
  email?: string | null;
  password?: string | null;
}

export interface BooleanResult {
  success?: boolean;
  message?: string | null;
  data?: boolean;
}

export interface ChangePasswordModel {
  /**
   * @format email
   * @minLength 1
   */
  email: string;
  /** @format int32 */
  code: number;
  /** @minLength 1 */
  newPassword: string;
}

export interface CreateRoleModel {
  name?: string | null;
}

export interface CreateUserModel {
  /** @minLength 1 */
  name: string;
  /**
   * @format email
   * @minLength 1
   */
  email: string;
  /**
   * @minLength 11
   * @maxLength 11
   * @pattern ^\d{11}$
   */
  cpf: string;
  /** @minLength 1 */
  password: string;
  phone?: string | null;
}

/** @format int32 */
export enum DayOfWeek {
  Value0 = 0,
  Value1 = 1,
  Value2 = 2,
  Value3 = 3,
  Value4 = 4,
  Value5 = 5,
  Value6 = 6,
}

export interface OperatingHoursModel {
  /** @format int32 */
  id?: number;
  dayOfWeek?: DayOfWeek;
  /** @format date-span */
  startTime?: string;
  /** @format date-span */
  endTime?: string;
}

export interface RoleModel {
  /** @format int32 */
  id?: number | null;
  name?: string | null;
}

export interface RoleModelResult {
  success?: boolean;
  message?: string | null;
  data?: RoleModel;
}

export interface RoomModel {
  /** @format int32 */
  id?: number;
  name?: string | null;
  operatingHours?: OperatingHoursModel[] | null;
}

export interface RoomModelIEnumerableResult {
  success?: boolean;
  message?: string | null;
  data?: RoomModel[] | null;
}

export interface TokenModel {
  accessToken?: string | null;
  /** @format int64 */
  expiresAccessToken?: number;
  refreshToken?: string | null;
  /** @format int64 */
  expiresRefreshToken?: number;
}

export interface TokenModelResult {
  success?: boolean;
  message?: string | null;
  data?: TokenModel;
}

export interface UserModel {
  name?: string | null;
  email?: string | null;
  cpf?: string | null;
  role?: string | null;
  phone?: string | null;
}

export interface UserModelResult {
  success?: boolean;
  message?: string | null;
  data?: UserModel;
}

export interface AccountReauthUpdateParams {
  refreshToken?: string;
}

export interface AccountForgotPasswordUpdateParams {
  email?: string;
}

import type { AxiosInstance, AxiosRequestConfig, AxiosResponse, HeadersDefaults, ResponseType } from "axios";
import axios from "axios";

export type QueryParamsType = Record<string | number, any>;

export interface FullRequestParams extends Omit<AxiosRequestConfig, "data" | "params" | "url" | "responseType"> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean;
  /** request path */
  path: string;
  /** content type of request body */
  type?: ContentType;
  /** query params */
  query?: QueryParamsType;
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseType;
  /** request body */
  body?: unknown;
}

export type RequestParams = Omit<FullRequestParams, "body" | "method" | "query" | "path">;

export interface ApiConfig<SecurityDataType = unknown> extends Omit<AxiosRequestConfig, "data" | "cancelToken"> {
  securityWorker?: (
    securityData: SecurityDataType | null,
  ) => Promise<AxiosRequestConfig | void> | AxiosRequestConfig | void;
  secure?: boolean;
  format?: ResponseType;
}

export enum ContentType {
  Json = "application/json",
  FormData = "multipart/form-data",
  UrlEncoded = "application/x-www-form-urlencoded",
  Text = "text/plain",
}

export class HttpClient<SecurityDataType = unknown> {
  public instance: AxiosInstance;
  private securityData: SecurityDataType | null = null;
  private securityWorker?: ApiConfig<SecurityDataType>["securityWorker"];
  private secure?: boolean;
  private format?: ResponseType;

  constructor({ securityWorker, secure, format, ...axiosConfig }: ApiConfig<SecurityDataType> = {}) {
    this.instance = axios.create({ ...axiosConfig, baseURL: axiosConfig.baseURL || "" });
    this.secure = secure;
    this.format = format;
    this.securityWorker = securityWorker;
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data;
  };

  protected mergeRequestParams(params1: AxiosRequestConfig, params2?: AxiosRequestConfig): AxiosRequestConfig {
    const method = params1.method || (params2 && params2.method);

    return {
      ...this.instance.defaults,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...((method && this.instance.defaults.headers[method.toLowerCase() as keyof HeadersDefaults]) || {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    };
  }

  protected stringifyFormItem(formItem: unknown) {
    if (typeof formItem === "object" && formItem !== null) {
      return JSON.stringify(formItem);
    } else {
      return `${formItem}`;
    }
  }

  protected createFormData(input: Record<string, unknown>): FormData {
    if (input instanceof FormData) {
      return input;
    }
    return Object.keys(input || {}).reduce((formData, key) => {
      const property = input[key];
      const propertyContent: any[] = property instanceof Array ? property : [property];

      for (const formItem of propertyContent) {
        const isFileType = formItem instanceof Blob || formItem instanceof File;
        formData.append(key, isFileType ? formItem : this.stringifyFormItem(formItem));
      }

      return formData;
    }, new FormData());
  }

  public request = async <T = any, _E = any>({
    secure,
    path,
    type,
    query,
    format,
    body,
    ...params
  }: FullRequestParams): Promise<AxiosResponse<T>> => {
    const secureParams =
      ((typeof secure === "boolean" ? secure : this.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {};
    const requestParams = this.mergeRequestParams(params, secureParams);
    const responseFormat = format || this.format || undefined;

    if (type === ContentType.FormData && body && body !== null && typeof body === "object") {
      body = this.createFormData(body as Record<string, unknown>);
    }

    if (type === ContentType.Text && body && body !== null && typeof body !== "string") {
      body = JSON.stringify(body);
    }

    return this.instance.request({
      ...requestParams,
      headers: {
        ...(requestParams.headers || {}),
        ...(type ? { "Content-Type": type } : {}),
      },
      params: query,
      responseType: responseFormat,
      data: body,
      url: path,
    });
  };
}

/**
 * @title Tech Solutions
 * @version v1
 *
 * Service Tech Solutions
 */
export class Api<SecurityDataType extends unknown> extends HttpClient<SecurityDataType> {
  api = {
    /**
     * No description
     *
     * @tags Account
     * @name AccountMeInfoList
     * @request GET:/api/Account/me-info
     * @secure
     * @response `200` `UserModelResult` OK
     */
    accountMeInfoList: (params: RequestParams = {}) =>
      this.request<UserModelResult, any>({
        path: `/api/Account/me-info`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Account
     * @name AccountRegisterCreate
     * @request POST:/api/Account/register
     * @secure
     * @response `200` `TokenModelResult` OK
     */
    accountRegisterCreate: (data: CreateUserModel, params: RequestParams = {}) =>
      this.request<TokenModelResult, any>({
        path: `/api/Account/register`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Account
     * @name AccountLoginCreate
     * @request POST:/api/Account/login
     * @secure
     * @response `200` `TokenModelResult` OK
     */
    accountLoginCreate: (data: AuthModel, params: RequestParams = {}) =>
      this.request<TokenModelResult, any>({
        path: `/api/Account/login`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Account
     * @name AccountReauthUpdate
     * @request PUT:/api/Account/reauth
     * @secure
     * @response `200` `TokenModelResult` OK
     */
    accountReauthUpdate: (query: AccountReauthUpdateParams, params: RequestParams = {}) =>
      this.request<TokenModelResult, any>({
        path: `/api/Account/reauth`,
        method: "PUT",
        query: query,
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Account
     * @name AccountForgotPasswordUpdate
     * @request PUT:/api/Account/forgot-password
     * @secure
     * @response `200` `void` OK
     */
    accountForgotPasswordUpdate: (query: AccountForgotPasswordUpdateParams, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Account/forgot-password`,
        method: "PUT",
        query: query,
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Account
     * @name AccountChangePasswordUpdate
     * @request PUT:/api/Account/change-password
     * @secure
     * @response `200` `void` OK
     */
    accountChangePasswordUpdate: (data: ChangePasswordModel, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Account/change-password`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Account
     * @name AccountUpdate
     * @request PUT:/api/Account
     * @secure
     * @response `200` `BooleanResult` OK
     */
    accountUpdate: (data: CreateUserModel, params: RequestParams = {}) =>
      this.request<BooleanResult, any>({
        path: `/api/Account`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Account
     * @name AccountDelete
     * @request DELETE:/api/Account
     * @secure
     * @response `200` `BooleanResult` OK
     */
    accountDelete: (params: RequestParams = {}) =>
      this.request<BooleanResult, any>({
        path: `/api/Account`,
        method: "DELETE",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Booking
     * @name BookingMeList
     * @request GET:/api/Booking/me
     * @secure
     * @response `200` `void` OK
     */
    bookingMeList: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Booking/me`,
        method: "GET",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Booking
     * @name BookingCreate
     * @request POST:/api/Booking
     * @secure
     * @response `200` `void` OK
     */
    bookingCreate: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Booking`,
        method: "POST",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Booking
     * @name BookingUpdate
     * @request PUT:/api/Booking
     * @secure
     * @response `200` `void` OK
     */
    bookingUpdate: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Booking`,
        method: "PUT",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Booking
     * @name BookingDelete
     * @request DELETE:/api/Booking
     * @secure
     * @response `200` `void` OK
     */
    bookingDelete: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Booking`,
        method: "DELETE",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Role
     * @name RoleCreate
     * @request POST:/api/Role
     * @secure
     * @response `200` `RoleModelResult` OK
     */
    roleCreate: (data: CreateRoleModel, params: RequestParams = {}) =>
      this.request<RoleModelResult, any>({
        path: `/api/Role`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Role
     * @name RoleDelete
     * @request DELETE:/api/Role/{id}
     * @secure
     * @response `200` `BooleanResult` OK
     */
    roleDelete: (id: number, params: RequestParams = {}) =>
      this.request<BooleanResult, any>({
        path: `/api/Role/${id}`,
        method: "DELETE",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Room
     * @name RoomList
     * @request GET:/api/Room
     * @secure
     * @response `200` `RoomModelIEnumerableResult` OK
     */
    roomList: (params: RequestParams = {}) =>
      this.request<RoomModelIEnumerableResult, any>({
        path: `/api/Room`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Room
     * @name RoomCreate
     * @request POST:/api/Room
     * @secure
     * @response `200` `void` OK
     */
    roomCreate: (data: RoomModel, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Room`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),
  };
}
