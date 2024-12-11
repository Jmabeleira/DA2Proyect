export class Utils {
  public static MapUserRole(role: number): number[] {
    if (role == 2) {
      return [0, 1];
    } else if (role == 1) {
      return [1];
    } else {
      return [0];
    }
  }
}
