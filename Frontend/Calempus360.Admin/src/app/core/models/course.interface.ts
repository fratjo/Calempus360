import { EquipmentType } from "./equipment.interface"

export type Course = {
    id?: string,
    name?: string,
    code?: string,
    description?: string,
    totalHours?: number,
    weeklyHours?: number,
    semester?: string,
    credits?: number,
    equipmentType?: any[],
}