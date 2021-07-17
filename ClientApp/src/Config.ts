const prod = {
    url: "/",
};

const dev = {
    url: "http://localhost",
};

export const config = process.env.NODE_ENV === "development" ? dev : prod;
