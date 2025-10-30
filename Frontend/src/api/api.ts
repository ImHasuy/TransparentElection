import axiosInstance from "./axios.config.ts";
import type ApiResponse from "../interfaces/ApiResponse.ts";
import type loginDto from "../interfaces/loginDto.ts";
import type LoginResponse from "../interfaces/LoginResponse.ts";


const Department = {
    getDepartments: () =>
        axiosInstance.get<ApiResponse<string[]>>("/api/Department/GetDepartments")
}

const Auth = {
    login: (dto: loginDto) =>
        axiosInstance.post<ApiResponse<LoginResponse>>("/api/Department/xxcvxcv", dto)

}
const api = {Department, Auth};

export default api;