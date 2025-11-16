import axiosInstance from "./axios.config.ts";
import type ApiResponse from "../interfaces/ApiResponse.ts";
import type loginDto from "../interfaces/loginDto.ts";
import type LoginResponse from "../interfaces/LoginResponse.ts";
import type FirstLayerPostInputDto from "@/interfaces/FirstLayerPostInputDto.ts";


const Department = {
    getDepartments: () =>
        axiosInstance.get<ApiResponse<string[]>>("/api/Department/GetDepartments")
}

const Auth = {
    login: (dto: loginDto) =>
        axiosInstance.post<ApiResponse<LoginResponse>>("/Bejelentkezes/login", dto),

    loginForVoters: (dto: FirstLayerPostInputDto) =>
        axiosInstance.post<ApiResponse<LoginResponse>>("/api/Auth/AuthenticateAsyncForVoter", dto)

}
const api = {Department, Auth};

export default api;