import React, { useState, useEffect } from 'react';
import go, { Diagram } from 'gojs';

export default function Graph({ selectedGraph }) {
  const [concepts, setConcepts] = useState(null);

  useEffect(() => {
    fetch(`/api/graphs/${selectedGraph.id}/concepts`)
      .then(response => response.json())
      .then(fetchedConcepts => {
        setConcepts(fetchedConcepts);
      });
  }, []);

  useEffect(() => {
    if (concepts) {
      const conceptsById = {};

      concepts.forEach(concept => conceptsById[concept.id] = concept);

      const $ = go.GraphObject.make;

      const diagram =
        $(go.Diagram, "diagram",
          { // enable undo & redo
            "undoManager.isEnabled": true,
            layout: $(go.LayeredDigraphLayout)
          });
  
          diagram.nodeTemplate =
        $(go.Node, "Auto",
          $(go.Shape, "RoundedRectangle",
            {
              strokeWidth: 0,
              fill: "lightgreen",
            }),
          $(go.TextBlock,
            { margin: 8 },
            new go.Binding("text", "name"))
        );
      
        diagram.model = new go.GraphLinksModel(
        Object.values(conceptsById).map(concept => ({
          key: concept.id,
          name: concept.name
        })),
        [].concat.apply([], Object.values(conceptsById)
          .map(concept => concept.dependenciesIds
            .map(dependencyId => ({
              from: dependencyId,
              to: concept.id
            }))
          )
        )
      );

      diagram.layout.direction = 90;

      return () => {
        diagram.div = null;
      };
    }
  }, [concepts]);

  return (
    <div>
      <div id="diagram" style={{border: "solid 1px black", background: "white", width: "100%", height: "500px"}}></div>

      {
        concepts
          ? concepts.map(concept =>
            <div>{concept.name} - {concept.description}</div>)
          : null
      }
    </div>
  );
}