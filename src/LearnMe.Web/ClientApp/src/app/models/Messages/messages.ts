export interface Messages {
    senderId: string;
    senderName: string;
    recipientId: string;
    recipientName: string;
    content: string;
    isRead: boolean;
    dateRead: Date;
    dateSent: Date;
}
