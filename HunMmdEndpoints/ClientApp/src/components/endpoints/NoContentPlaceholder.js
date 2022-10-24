import React, { useState } from 'react';

export function NoContentPlaceholder(props) {
    return (
        <div>
            <div className="spinner-border text-primary" role="status">
                <span className="visually-hidden">Loading...</span>
            </div>
            <div className="spinner-border text-secondary" role="status">
                <span className="visually-hidden">Loading...</span>
            </div>
            <div className="spinner-border text-success" role="status">
                <span className="visually-hidden">Loading...</span>
            </div>
        </div>
    );
}

NoContentPlaceholder.displayName = 'NoContentPlaceholder';
