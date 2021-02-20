export interface Messages {
    senderId: string;
    senderFirstName: string;
    senderLastName: string;
    senderEmail: string;
    recipientId: string;
    recipientFirstName: string;
    recipientLastName: string;
    recipientEmail: string;
    title: string;
    content: string;
    isRead: boolean;
    dateRead: Date;
    dateSent: Date;
    imgPath: string;
}
