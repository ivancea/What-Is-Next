import React, { useState } from "react";
import { Graph as GraphType } from "../types/graph";
import ApiListSelector from "./ApiListSelector";
import Graph from "./Graph";

export function Home(): React.ReactElement {
    const [selectedTopic, setSelectedTopic] = useState<string>(); // This should fail
    const [selectedGraph, setSelectedGraph] = useState<GraphType>();

    function handleSelectTopic(topic?: string): void {
        setSelectedTopic(topic);
        setSelectedGraph(undefined);
    }

    return (
        <div>
            <ApiListSelector
                url="/api/topics"
                elementName="topic"
                keySelector={(topic) => topic}
                render={(topic) => topic}
                selectedElement={selectedTopic}
                onElementSelected={handleSelectTopic}
            />

            {selectedTopic ? (
                <>
                    <hr />
                    <ApiListSelector
                        url={`/api/topics/${encodeURIComponent(selectedTopic)}/graphs`}
                        elementName="graph"
                        keySelector={(graph) => graph.id}
                        render={(graph) => `${graph.name} (${graph.description})`}
                        selectedElement={selectedGraph}
                        onElementSelected={setSelectedGraph}
                    />

                    {selectedGraph ? (
                        <>
                            <hr />
                            <Graph selectedGraph={selectedGraph} />
                        </>
                    ) : null}
                </>
            ) : null}
        </div>
    );
}
