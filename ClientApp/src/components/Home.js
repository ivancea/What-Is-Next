import React, { useState } from 'react';
import ApiListSelector from "./ApiListSelector.js";
import Graph from "./Graph.js";

export function Home() {
  const [ selectedTopic, setSelectedTopic ] = useState(null);
  const [ selectedGraph, setSelectedGraph ] = useState(null);

  function handleSelectTopic(topic) {
    setSelectedTopic(topic);
    setSelectedGraph(null);
  }

  return (
    <div>
      <ApiListSelector
        url="/api/topics"
        elementName="topic"
        keySelector={topic => topic}
        render={topic => topic}
        selectedElement={selectedTopic}
        onElementSelected={handleSelectTopic} />

      {
        selectedTopic
          ? <>
              <hr />
              <ApiListSelector
                url={`/api/topics/${encodeURIComponent(selectedTopic)}/graphs`}
                elementName="graph"
                keySelector={graph => graph.id}
                render={graph => `${graph.name} (${graph.description})`}
                selectedElement={selectedGraph}
                onElementSelected={setSelectedGraph} />

                {
                  selectedGraph
                    ? <>
                      <hr />
                      <Graph selectedGraph={selectedGraph} />
                    </>
                    : null
                }
              </>
          : null
      }
    </div>
  );
}
