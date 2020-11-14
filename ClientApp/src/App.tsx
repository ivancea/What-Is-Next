import React from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";

export default function App(): React.ReactElement {
    return (
        <Layout>
            <Route path="/" component={Home} />
        </Layout>
    );
}
