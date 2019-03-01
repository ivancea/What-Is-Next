import React, { useState, useEffect } from 'react';

export default function TopicSelector({ url, elementName, keySelector, render, selectedElement, onElementSelected }) {
    const [ elements, setElements] = useState(null);

    useEffect(() => {
        fetch(url)
            .then(response => response.json())
            .then(setElements);
    }, [url]);

    if (selectedElement) {
        return (
            <div>
                <button onClick={() => onElementSelected(null)}>Back</button>
                Selected {elementName}: {render(selectedElement)}
            </div>
        );
    }

    return elements
        ? <div style={{cursor: "pointer"}}>
            {elements.map(element =>
                <div key={keySelector(element)} onClick={() => onElementSelected(element)}>
                    {render(element)}
                </div>)}
        </div>
        : null;
}