import { Dialog } from 'evergreen-ui';
import React from 'react';

interface DeleteConfirmationDialogProps {
    open: boolean;
    onSubmit: () => void;
    onClose: () => void;
}

export const ConfirmationDialog: React.FC<DeleteConfirmationDialogProps> = ({ open, onSubmit, onClose }) => {
    return (
        <Dialog isShown={open} title="Delete confirmation" intent="danger" onConfirm={onSubmit} onCloseComplete={onClose} confirmLabel={'Delete'}>
            If you will do this action, you won't be able to revert it.
        </Dialog>
    );
};
