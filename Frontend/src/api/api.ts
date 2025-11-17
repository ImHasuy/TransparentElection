import axiosInstance from "./axios.config.ts";
import type ApiResponse from "../interfaces/ApiResponse.ts";
import type loginDto from "../interfaces/loginDto.ts";
import type LoginResponse from "../interfaces/LoginResponse.ts";
import type FirstLayerPostInputDto from "@/interfaces/FirstLayerPostInputDto.ts";
import type QRCodeDecodeDto from "@/interfaces/QRCodeDecodeDto.ts";
import type PartyListGetDto from "@/interfaces/PartyListGetDto.ts";


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

const QrCode = {
    QRCodeScan: (dto: QRCodeDecodeDto) =>
        axiosInstance.post<ApiResponse<LoginResponse>>("/api/QRCodeGeneration/QRCodeScan", dto),
}

const PartyList = {
    GetPartyList: () =>
        axiosInstance.get<ApiResponse<PartyListGetDto>>("/api/PartyList/GetPartyList"),
}


const api = {Department, Auth, QrCode,PartyList};

export default api;