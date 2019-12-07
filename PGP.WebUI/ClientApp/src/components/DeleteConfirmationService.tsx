import * as React from 'react';
import { useState } from 'react';
import { ConfirmationDialog } from './DeleteConfirmationDialog';

const DeleteConfirmationServiceContext = React.createContext<() => Promise<void>>(Promise.reject);

export const useDeleteConfirmation = () => React.useContext(DeleteConfirmationServiceContext);

export const DeleteConfirmationServiceProvider: React.FC = props => {
    const [isOpen, setIsOpen] = useState<boolean>(false);

    const awaitingPromiseRef = React.useRef<{
        resolve: () => void;
        reject: () => void;
    }>();

    const openConfirmation = () => {
        setIsOpen(true);

        return new Promise<void>((resolve, reject) => {
            awaitingPromiseRef.current = { resolve, reject };
        });
    };

    const handleClose = () => {
        if (awaitingPromiseRef.current) {
            awaitingPromiseRef.current.reject();
        }

        setIsOpen(false);
    };

    const handleSubmit = () => {
        if (awaitingPromiseRef.current) {
            awaitingPromiseRef.current.resolve();
        }

        setIsOpen(false);
    };

    return (
        <>
            <ConfirmationDialog open={isOpen} onSubmit={handleSubmit} onClose={handleClose} />
            <DeleteConfirmationServiceContext.Provider value={openConfirmation}>{props.children}</DeleteConfirmationServiceContext.Provider>
        </>
    );
};
