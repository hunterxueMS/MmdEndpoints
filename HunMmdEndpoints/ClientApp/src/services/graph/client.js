import axios from "axios";

export const graphHttpClient = axios.create({
    baseURL: "https://graph.microsoft.com/v1.0/",
    headers: {
        "Content-Type": "application/json",
    },
});

async function getMe() {
    const response = await graphHttpClient.get(`me`);
    return response.data;
}

async function getBase64Photo() {
    const response = await graphHttpClient.get(`me/photos/64x64/$value`, {
        responseType: 'arraybuffer'
    });

    const buffer = response.data;
    const binaryString = Array.from(new Uint8Array(buffer), byte => String.fromCharCode(byte)).join("");
    return `data:image/jpeg;base64,${btoa(binaryString)}`;
}



export { getMe, getBase64Photo };
