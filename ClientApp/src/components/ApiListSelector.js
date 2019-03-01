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
                <b>Selected {elementName}:</b> {render(selectedElement)}
            </div>
        );
    }

    return elements
        ? <ul>
            {elements.map(element =>
                <li style={{cursor: "pointer"}} key={keySelector(element)} onClick={() => onElementSelected(element)}>
                    {render(element)}
                </li>)}
        </ul>
        : null;
}